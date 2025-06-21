# FiveKenPo
Programa que analise o resultado de múltiplos jogadores em um jogo de jokenpo.

## Execução
### Comandos
Com o repositório clonado, vá para o diretório FiveKenPo, compile e execute:
```
cd FiveKenPo
dotnet build
dotnet run
```

### Testes
Para executar os testes, use o seguinte comando:
```
dotnet test
```

## Endpoints (API Routes)
Com a execução do programa, o endereço da API é mostrado no terminal.\
Por meio desse endereço você pode acessar as rotas abaixo:
```
POST /api/Game/player
DELETE /api/Game/player
POST /api/Game/move
GET /api/Game/round
POST /api/Game/finish
```
Algumas rotas requerem o envio de um JSON no corpo da requisição:\
Para adicionar jogador, faça a requisição `POST /api/Game/player` com o corpo no formato:
```json
{
  "name": "Nome do Jogador"
}
```
Para remover jogador, faça a requisição `DELETE /api/Game/player` com o corpo no formato:
```json
{
  "name": "Nome do Jogador"
}
```
Para adicinar uma jogada, faça a requisição `POST /api/Game/move` com o corpo no formato:
```json
{
  "name": "Nome do Jogador",
  "move": "Spock"
}
```
As jogadas válidas são: "Rock", "Paper", "Scissors", "Lizard" e "Spock".

## Interfaces para Requisições
Você pode usar algumas interfaces prontas para facilidar nas requisições:
- Swagger: acesse o endereço da API e adicione `/swagger` ao final para acessar a interface do Swagger.
- Insomnia: você pode importar o arquivo InsomniaRequests.yaml com as rotas se tiver o Insomnia instalado.

## Detalhes sobre o Jogo
- O jogo aceita quantidade "ilimitada" de jogadores, depende da memória disponível.
- Não foi utilizada um banco de dados, os dados são armazenados em memória.
- Quando uma jogada é registrada, ela não pode ser alterada para evitar que outro jogador mude por ele.
- O vencedor da rodada é definido pelo jogador que derrotou mais quantidades de adversários.