using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    // for the first enemy in the wave
    [SerializeField] int startingIndex = 0;

    // for a list of waves
    [SerializeField] List<WaveConfig> waveConfigOfSpawn;
    [SerializeField] bool looping = false; // looping after all waves are done

    [SerializeField] float timeBetweenWaves = 12f;

    [SerializeField] TextEntryUI annoucementText;

    IEnumerator Start()
    {
        GameManager.Instance.HasWin += TurnOffAnnoucement;
        do
        {
            //wait til all waves are spawn until do it all over again
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    void TurnOffAnnoucement()
    {
        var tmpro = annoucementText.GetComponent<TextMeshProUGUI>();
        tmpro.enabled = false;
    }

    private IEnumerator SpawnEnemy(WaveConfig wave)
    {
        for (int enemyCount = 1; enemyCount <= wave.numberOfEnemies; enemyCount++)
        {
            //instantiate new enemy from the wave config information
            var newEnemy = Instantiate(
                wave.EnemyPrefab,
                wave.GetWaypoints()[startingIndex].transform.position,
                Quaternion.identity, transform);

            //set the wave to the enemy that gets spawn so that it knows which wave to follow
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(wave);
            if (GameManager.Instance.Win)
            {
                newEnemy.GetComponent<Animator>().enabled = false;
            }
            yield return new WaitForSeconds(wave.GetRandomSpawnTime());
        }
    }


    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingIndex; i < waveConfigOfSpawn.Count; i++)
        {

            // SetWaveConfig
            var currentWave = waveConfigOfSpawn[i];

            //wait for one wave to terminate til the next wave
            yield return StartCoroutine(SpawnEnemy(currentWave));

            annoucementText.SetUpText(new TextEntry("Next wave is coming"));
            annoucementText.Show();
            annoucementText.Animate();

            yield return new WaitForSeconds(timeBetweenWaves);
        }

    }

}
