using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public delegate void OnLevelFinishedType();
    public static event OnLevelFinishedType OnLevelFinished;

    [SerializeField] private GameObject levelFinishedPrompt;
    private const int blocksLayer = 6;
    private int allBlocksCount;

    void Start()
    {
        Block.OnBlockDestroyed += CheckLevelEnded;
        allBlocksCount = FindGameObjectsInLayer(blocksLayer).Count;
    }

    void OnDestroy()
    {
        Block.OnBlockDestroyed -= CheckLevelEnded;
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene($"Level {level}", LoadSceneMode.Single);
    }

    private void CheckLevelEnded(float _resiliencePoints)
    {
        allBlocksCount--;
        Debug.Log("Blocks left: " + allBlocksCount);
        if (allBlocksCount <= 0)
        {
            Debug.Log("Level finished!");
            OnLevelFinished?.Invoke();
            levelFinishedPrompt.SetActive(true);
        }
    }

    List<GameObject> FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        return goArray.ToList().FindAll((gameObject) => gameObject.layer == layer);
    }
}
