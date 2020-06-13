using TMPro;
using TowerDefence.GameController.Inventory;
using TowerDefence.Units.Throne;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence.GameController
{
    public class GameController : MonoBehaviour
    {
        public GameObject StartGamePanel;
        public GameObject EndGamePanel;
        public TMP_Text EndGameText;
        public SpawnerController spawnerController;
        public InventoryController inventoryController;
        public Throne throne;

        public void StartGame()
        {
            StartGamePanel.SetActive(false);
            inventoryController.Init();
            spawnerController.Init();
            throne.OnDeathEvent += OnThroneDeath;
            spawnerController.FinishAllWavesEvent += EndGame;
        }

        private void OnThroneDeath(GameObject _)
        {
            EndGame(false);
        }

        private void EndGame(bool didWin)
        {
            EndGameText.text = didWin ? "You Win" : "You Lose";
            EndGamePanel.SetActive(true);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
