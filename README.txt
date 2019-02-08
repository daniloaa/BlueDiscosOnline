Projeto BlueOnlineDiscos.

###########################
Etapa 01 - Gênero 

A primeira etapa que dever realizada é a inserção dos gêneros dos albums. 
A chamada da API "PostGenero" irá popular o banco de dados com as informações de gênero e o percentual cashback por dia da semana.
Abaixo segue URL da API e um JSON exemplo seguindo a tabela informada no desafio:

Url: https://localhost:44369/api/v1/Genero/PostGenero
Método: POST
Content-Type: application/json
JSON BODY:
[
  {
    "Descricao": "pop",
    "PercentualDia": [
      {
        "DiaSemana": 0,
        "Percentual": 25
      },
      {
        "DiaSemana": 1,
        "Percentual": 7
      },
      {
        "DiaSemana": 2,
        "Percentual": 6
      },
      {
        "DiaSemana": 3,
        "Percentual": 2
      },
      {
        "DiaSemana": 4,
        "Percentual": 10
      },
      {
        "DiaSemana": 5,
        "Percentual": 15
      },
      {
        "DiaSemana": 6,
        "Percentual": 20
      }
    ]
  },
  {
    "Descricao": "mpb",
    "PercentualDia": [
      {
        "DiaSemana": 0,
        "Percentual": 30
      },
      {
        "DiaSemana": 1,
        "Percentual": 5
      },
      {
        "DiaSemana": 2,
        "Percentual": 10
      },
      {
        "DiaSemana": 3,
        "Percentual": 15
      },
      {
        "DiaSemana": 4,
        "Percentual": 20
      },
      {
        "DiaSemana": 5,
        "Percentual": 25
      },
      {
        "DiaSemana": 6,
        "Percentual": 30
      }
    ]
  },
  {
    "Descricao": "classical",
    "PercentualDia": [
      {
        "DiaSemana": 0,
        "Percentual": 35
      },
      {
        "DiaSemana": 1,
        "Percentual": 3
      },
      {
        "DiaSemana": 2,
        "Percentual": 5
      },
      {
        "DiaSemana": 3,
        "Percentual": 8
      },
      {
        "DiaSemana": 4,
        "Percentual": 13
      },
      {
        "DiaSemana": 5,
        "Percentual": 18
      },
      {
        "DiaSemana": 6,
        "Percentual": 25
      }
    ]
  },
  {
    "Descricao": "rock",
    "PercentualDia": [
      {
        "DiaSemana": 0,
        "Percentual": 40
      },
      {
        "DiaSemana": 1,
        "Percentual": 10
      },
      {
        "DiaSemana": 2,
        "Percentual": 15
      },
      {
        "DiaSemana": 3,
        "Percentual": 15
      },
      {
        "DiaSemana": 4,
        "Percentual": 15
      },
      {
        "DiaSemana": 5,
        "Percentual": 20
      },
      {
        "DiaSemana": 6,
        "Percentual": 40
      }
    ]
  }  
]

Fim da Etapa 01 - Gênero

###########################

Etapa 02 - Data Spotify

A segunda etapa que deverá ser realizada é o consumo da API do Spotify, segundo os gêneros cadastrados no banco de dados (Etapa 01).
No arquivo "appsettings.json" existe o atributo "SearchAlbumLimite" que serve para configuração da quantidade de albuns por gênero que será consumido.
Abaixo segue informações sobre a API:

Url: https://localhost:44369/api/v1/Spotify/GetDataSpotify
Método: GET

Fim da Etapa 02 - Data Spotify

###########################

Etapa 03 - APIs solicitadas

1 - Consultar o catálogo de discos de forma paginada, filtrando por gênero e ordenando de forma crescente pelo nome do disco:

URL: https://localhost:44369/api/v1/Venda/GetAlbumPaginated 
Método: Get
Content-Type: application/json
Body Json: 
{
	"GeneroId" : 1,
	"Skip": 5,
	"Take": 10
}

2 - Consultar o disco pelo seu identificador:

URL: https://localhost:44369/api/v1/Album/{id}
Método: GET

3 - Consultar todas as vendas efetuadas de forma paginada, filtrando pelo range de datas (inicial e final) da venda e ordenando de forma decrescente pela
data da venda;

URL: https://localhost:44369/api/v1/Venda/GetVendaPaginated
Método: GET
Body Json:
{
	"DataInicial" : "01/02/2019",
	"DataFinal": "02/28/2019",
	"Skip" : 1,
	"Take": 10
}

4 - Consultar uma venda pelo seu identificador;

URL:  https://localhost:44369/api/v1/Venda/{id}
Método: GET

5 - Registrar uma nova venda de discos calculando o valor total de cashback considerando a tabela.

URL: https://localhost:44369/api/v1/Venda/NovaVenda/
Método: POST
Body: List de identificadores de albuns. Ex: [1,2,3,4,5]

Fim da Etapa 03

###########################

Informaçoes gerais

Definir BlueDiscosOnline.API como projeto principal para executar.

Arquivos do POSTMAN com as chamadas das APIs na pasta POSTMAN na raiz do projeto.

Os retornos da API são no formato de um container com os seguintes atributos: 
Status: Status sobre a requisição. (Success = 0, Info = 1, Warning = 2, Danger = 3 e Critical = 4)
Data: Retorno de algum dado caso seja necessário 
Messages: Mensagens de Erro
Exception: Exceções 
