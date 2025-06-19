# Desafio: JOKENPO - DEV

## Descrição do problema:

Programa que analise o resultado de múltiplos jogadores em um jogo de jokenpo.

![image](https://gist.github.com/assets/33282336/3c1fff16-6d99-4949-b887-9b180f41c1f6)


Os jogadores deverão informar as entradas através das jogadas e 
o sistema deverá indicar qual o jogador ganhador. 
 
As entradas das jogadas são disponibilizadas através de APIs REST, 
além da aplicação disponibilizar APIs para realização do cadastro dos jogadores e das jogadas também tem a possibilidade de consulta-los e excluí-los.

## Funcionalidades:

O sistema consiste em uma API REST, que irá armazenar informações sobre uma rodada de jokenpo.

Para isso, o sistema deverá contar com as seguintes funcionalidades:

- Cadastrar um jogador:

  Um jogador consiste em um nome e id.

- Remover um jogador:

  Um jogador pode ser removido da jogada a qualquer momento.

- Cadastrar a jogada:

  Um jogador pode cadastrar a sua jogada para a rodada atual com uma das 5 jogadas permitidas.

- Consultar a rodada atual:

  Para saber a situação da rodada atual é necessário consultar:
    - Quais os jogadores cadastrados
    - Quais os jogadores já jogaram e quais não jogaram.

- Finalização da rodada:

  Para encerrar a rodada atual após todos os jogadores jogarem e descobrir qual o vencedor.


## Orientações técnicas:

- Toda a comunicação é feita via JSON;
- Utilização das melhores práticas para a linguagem escolhida;
- Utilização de banco de dados não é obrigatória;
- Implementação de testes unitários são um bônus.

## Exemplos esperados:

### Cenário 1 (Sucesso)

 - Chamada 1: Cadastro jogador 1 – Carlos
 - Chamada 2: Jogada jogador 1 - Pedra 
 - Chamada 3: Cadastro jogador 2 – João
 - Chamada 4: Jogada jogador 2 - Tesoura 
 - Chamada 5: Cadastro jogador 3 – Matheus
 - Chamada 6: Consulta da rodada - Carlos já jogou, João já jogou, Matheus precisa jogar
 - Chamada 7: Jogada jogador 3 - Tesoura 
 - Chamada 8: Finalizada jogada - *Vencedor: Carlos*

### Cenário 2 (Falha)

 - Chamada 1: Cadastro jogador 1 – Carlos
 - Chamada 2: Jogada jogador 1 - Pedra 
 - Chamada 3: Cadastro jogador 2 – João
 - Chamada 4: Jogada jogador 2 - Tesoura 
 - Chamada 5: Cadastro jogador 3 – Matheus
 - Chamada 6: Finalizada jogada - *Erro: Matheus ainda não jogou*

### Cenário 3 (Falha)

 - Chamada 1: Jogada jogador 1 - *Erro: Jogador não cadastrado*

## Dicas:

- Documente todo o seu sistema, desde como fazer o setup e rodar a aplicação, até as rotas criadas, exemplos de chamadas à API e as decisões arquiteturais;

- Utilize Git, com commits pequenos e bem descritos (nada de um commit único com todo o código);
