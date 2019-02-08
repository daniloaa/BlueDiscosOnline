using BlueDiscosOnline.Common;
using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.Models;
using BlueDiscosOnline.Domain.ResponseModel;
using BlueDiscosOnline.Services.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Services.Services
{
    public class VendaService : BaseService<Venda>, IVendaService
    {
        public VendaService( IUnitOfWorkFactory unitOfWorkFactory,
                             IStringLocalizer<LanguageLocalizer> localizer
                             )
            : base(unitOfWorkFactory, localizer)
        {
            
        }

        public async Task<RequestResult> NovaVenda(List<int> vendaItensId)
        {
            var result = new RequestResult(StatusResult.Success);

            try
            {
                var novaVenda = new Venda();
                var data = DateTime.Now;
                var dayWeek = (int)data.DayOfWeek;

                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    foreach (var item in vendaItensId)
                    {
                        var album = unitOfWork.Repository.Get<Album>(x => x.Id == item, "Genero").FirstOrDefault();

                        if (album == null)
                        {
                            result.Status = StatusResult.Danger;
                            result.Messages.Add(new Message(string.Format(_localizer["ItemVendaError"], item)));
                        }
                        else
                        {
                            var percentualDia = unitOfWork.Repository.Get<GeneroPercentualDia>(x => x.GeneroId == album.GeneroId && x.DiaSemana == dayWeek ).FirstOrDefault();
                            if (percentualDia == null)
                            {
                                result.Status = StatusResult.Danger;
                                result.Messages.Add(new Message(string.Format(_localizer["PercentualAlbumError"], item)));
                            }
                            else
                            {
                                var cashback = Math.Round((album.Valor * percentualDia.Percentual) / 100, 2);

                                novaVenda.Itens.Add(new VendaItem { AlbumId = album.Id, ValorCashBack = cashback });
                            }
                        }
                    }

                    if (result.Status == StatusResult.Success)
                    {
                        novaVenda.ValorCashBackTotal = novaVenda.Itens.Sum(x => x.ValorCashBack);

                        novaVenda.DataVenda = DateTime.UtcNow;

                        unitOfWork.Repository.Add(novaVenda);

                        await unitOfWork.Commit();

                        result.Data = novaVenda;
                    }                 
                }
            }

            catch (Exception ex)
            {
                result.Status = StatusResult.Danger;
                result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"], ex.Message)));
            }

            return result;
        }

        public async Task<RequestResult> GetVendaPaginated(VendaModel vendaModel)
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork())
                {
                    var vendaQuantidade = await unitOfWork.Repository.Count<Venda>(x => x.DataVenda >= vendaModel.DataInicial && x.DataVenda <= vendaModel.DataFinal);

                    var albumList = unitOfWork.Repository.Get<Venda>(x => x.DataVenda >= vendaModel.DataInicial && x.DataVenda <= vendaModel.DataFinal).OrderByDescending(x => x.DataVenda)
                                                                                                            .Skip(vendaModel.Skip)
                                                                                                            .Take(vendaModel.Take).ToList();
                    var resultPaginated = new VendaResponse { Venda = albumList, Quantidade = vendaQuantidade };

                    result.Data = resultPaginated;
                }

            }
            catch (Exception ex)
            {
                result.Status = StatusResult.Danger;
                result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"], ex.Message)));
            }

            return result;
        }
    }
}
