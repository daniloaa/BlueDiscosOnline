using BlueDiscosOnline.Common.Containers;
using BlueDiscosOnline.Common.Enumerators;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Domain.ResponseModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Services.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration Configuration;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IGeneroService _generoService;

        public SpotifyService( IHttpClientFactory httpClientFactory,
                               IConfiguration configuration,
                               IUnitOfWorkFactory unitOfWorkFactory,
                               IGeneroService generoService
                             )           
        {
            _httpClientFactory = httpClientFactory;
            Configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
            _generoService = generoService;
        }

        public async Task<RequestResult> GetDataSpotify()
        {
            var result = new RequestResult(StatusResult.Success);
            try
            {
                
                var autorization = await GetAutorization();

                if (autorization != null)
                {
                    var generosList = _generoService.GetAllGenero();
                    var albumList = new List<Album>();
                    Random numRandom = new Random();

                    using (var client = _httpClientFactory.CreateClient("BlueDiscosOnlineClient"))
                    {
                        string baseURL = Configuration.GetSection("SpotifyConnection:BaseURL").Value;
                        client.BaseAddress = new Uri(baseURL);
                        var searchAlbumLimite = Configuration.GetSection("SpotifyConnection:SearchAlbumLimite").Value;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autorization.Access_token);

                        foreach (var item in generosList)
                        {
                            var spotifyFilter = new SpotifyFilter();
                            var url = client.BaseAddress + "recommendations?limit=" + searchAlbumLimite + "&seed_genres=" + item.Descricao;

                            var response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();
                            if (response.IsSuccessStatusCode)
                            {
                                string content = await response.Content.ReadAsStringAsync();
                                if (content != null)
                                {
                                    spotifyFilter = JsonConvert.DeserializeObject<SpotifyFilter>(content);
                                }
                            }

                            foreach (var album in spotifyFilter.Tracks)
                            {
                                albumList.Add(new Album { Nome = album.Album.Name, Artista = album.Artists.FirstOrDefault().Name, GeneroId = item.Id, Valor = Convert.ToDecimal(numRandom.Next(1, 99) + Math.Round(numRandom.NextDouble(), 2)) });
                            }
                        }
                    }

                    if (albumList.Count() > 0)
                    {
                        using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                        {
                            foreach (var album in albumList)
                            {
                                unitOfWork.Repository.Add(album);
                            }

                            await unitOfWork.Commit();
                        }
                    }

                    result.Data = albumList;
                }
            }
            catch (Exception)
            {
                result.Status = StatusResult.Danger;
            }

            return result;

        }

        public async Task<SpotifyToken> GetAutorization()
        {
            using (var client = _httpClientFactory.CreateClient("BlueDiscosOnlineClient"))
            {
                var spotifyToken = new SpotifyToken();

                var spotifyId = Configuration["SpotifyConnection:SpotifyId"];
                var spotifyPw = Configuration["SpotifyConnection:SpotifyPW"];
                string baseURL = Configuration.GetSection("SpotifyConnection:BaseAutenticationURL").Value;
                var dict = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                };
                client.BaseAddress = new Uri(baseURL);

                var byteArray = Encoding.ASCII.GetBytes(spotifyId + ":" + spotifyPw);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var stringContent = new StringContent("grant_type:client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                var url = client.BaseAddress + "token";

                var response = await client.PostAsync(url, new FormUrlEncodedContent(dict));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        spotifyToken = JsonConvert.DeserializeObject<SpotifyToken>(content);
                    }                   
                }
                return spotifyToken;
            }
        }
    }
}
