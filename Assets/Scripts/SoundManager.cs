using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public float masterVolume;
    public AudioListener listener;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    public float efxvolume;
    public float musicvolume;
    public int currentscene=0;
    //public float lowPitchRange = 0.95f;
    //public float highPitchRange = 1.05f;
    // Use this for initialization

    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        musicSource.ignoreListenerVolume = true;
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        listener = GameObject.FindWithTag("MainCamera").GetComponent<AudioListener>();
    }

    public void ChangeEfxVolume()
    {
        efxvolume = masterVolume * GameObject.Find("Sound Effect Volume Slider").GetComponent<Slider>().value;
        AudioListener.volume = efxvolume;
    }

    public void ChangeMusicVolume()
    {
        musicvolume = masterVolume * GameObject.Find("Music Volume Slider").GetComponent<Slider>().value;
        musicSource.volume = musicvolume;
    }

    public void ChangeMasterVolume()
    {
        masterVolume = GameObject.Find("Master Volume Slider").GetComponent<Slider>().value;
        ChangeMusicVolume();
        ChangeEfxVolume();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.Stop();
		if (clip != null) {
			musicSource.clip = clip;
			musicSource.Play ();
		}
    }
    
    //public void PlaySingle(AudioClip clip)
    //{
    //    efxSource.clip = clip;
    //    efxSource.Play();
    //}

    //public void PlaySingleRandomized(AudioClip clip)
    //{
    //    float randomPitch = Random.Range(lowPitchRange, highPitchRange);
    //    efxSource.pitch = randomPitch;
    //    efxSource.clip = clip;
    //    efxSource.Play();
    //}
    
    //public void RandomizeSfx(params AudioClip[] clips)
    //{
    //    int randomIndex = Random.Range(0, clips.Length);
    //    float randomPitch = Random.Range(lowPitchRange, highPitchRange);

    //    efxSource.pitch = randomPitch;
    //    efxSource.clip = clips[randomIndex];
    //    efxSource.Play();
    //}
        // Update is called once per frame
	void Update () {
         
    }
}
