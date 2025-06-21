# FiveKenPo
Programa que analise o resultado de múltiplos jogadores em um jogo de jokenpo.

## Execução
### Requisitos
- .NET 8.0 SDK ou superior
- C# 12.0 ou superior
- Ferramentas de linha de comando do .NET

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
Com a execução do programa, é apresentado no terminal o endereço da API.\
Por meio desse endereço você pode acessar as rotas abaixo:
```
POST /api/Game/player
DELETE /api/Game/player/{name}
POST /api/Game/move
GET /api/Game/round
POST /api/Game/finish
```
Algumas rotas requerem o envio de um JSON no corpo da requisição:

### POST /api/Game/player
```json
{
  "name": "Nome do Jogador"
}
```

### POST /api/Game/move
As jogadas válidas são: "Rock", "Paper", "Scissors", "Lizard" e "Spock".\
```json
{
  "name": "Nome do Jogador",
  "move": "Spock"
}
```

## Detalhes sobre o Jogo
- O jogo aceita quantidade "ilimitada" de jogadores, depende da memória disponível.
- Não foi utilizada um banco de dados, os dados são armazenados em memória como Singleton.
- Quando uma jogada é registrada não pode ser alterada, para evitar que outro jogador mude por ele.
- O vencedor da rodada é definido pelo jogador que derrotou mais quantidade de adversários.