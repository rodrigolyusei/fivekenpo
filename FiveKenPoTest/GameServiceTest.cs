using FiveKenPo.Services;
using FiveKenPo.Models;

namespace FiveKenPoTest
{
    public sealed class GameServiceTest
    {
        [Fact]
        public void AddPlayer_ShouldAddAndAbleToGet()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Nome longo para teste unitário";
            // Act
            service.AddPlayer(playerName);
            var player = service.GetPlayer(playerName);
            // Assert
            Assert.NotNull(player);
            Assert.Equal(playerName, player.Name);
        }

        [Fact]
        public void AddPlayer_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var service = new GameService();
            string playerName = string.Empty;
            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.AddPlayer(playerName));
        }

        [Fact]
        public void AddPlayer_ShouldThrowException_WhenPlayerAlreadyExists()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Jogador Existente";
            service.AddPlayer(playerName);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.AddPlayer(playerName));
        }

        [Fact]
        public void RemovePlayer_ShouldRemove_ThrowException_WhenTryToGet()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Jogador para Remover";
            service.AddPlayer(playerName);
            // Act
            service.RemovePlayer(playerName);
            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetPlayer(playerName));
        }

        [Fact]
        public void AddMove_ShouldAddMove_WhenValid()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Jogador com Movimento";
            service.AddPlayer(playerName);
            string move = "Spock";
            // Act
            service.AddMove(playerName, move);
            var player = service.GetPlayer(playerName);
            // Assert
            Assert.Equal(Enum.Parse<MoveType>(move, true), player.Move);
        }

        [Fact]
        public void AddMove_ShouldThrowException_WhenPlayerNotFound()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Jogador Inexistente";
            string move = "Spock";
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => service.AddMove(playerName, move));
        }

        [Fact]
        public void AddMove_ShouldThrowException_WhenMoveIsInvalid()
        {
            // Arrange
            var service = new GameService();
            string playerName = "Jogador com Movimento Inválido";
            service.AddPlayer(playerName);
            string invalidMove = "Yoda";
            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.AddMove(playerName, invalidMove));
        }

        [Fact]
        public void FinishRound_ShouldReturnWinner_WhenValid()
        {
            // Arrange
            var service = new GameService();
            service.AddPlayer("Vencedor");
            service.AddPlayer("Perdedor");
            service.AddMove("Vencedor", "Lizard");
            service.AddMove("Perdedor", "Spock");
            // Act
            var winner = service.FinishRound();
            // Assert
            Assert.NotNull(winner);
            Assert.Equal("Vencedor", winner.Name);
        }

        [Fact]
        public void FinishRound_ShouldThrowException_WhenNotEnoughPlayers()
        {
            // Arrange
            var service = new GameService();
            service.AddPlayer("Sozinho");
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.FinishRound());
        }

        [Fact]
        public void FinishRound_ShouldThrowException_WhenPlayerNotPlayed()
        {
            // Arrange
            var service = new GameService();
            service.AddPlayer("Jogador Sem Movimento Ichi");
            service.AddPlayer("Jogador Sem Movimento Ni");
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.FinishRound());
        }
    }
}
