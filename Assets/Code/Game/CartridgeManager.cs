using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartridgeManager : MonoBehaviour
{
    public int CartdrigeToGetInLevel = 5;
    public List<CartridgeType> OwnedCartridges { get; set; }
    private GameObject _gottenCartridgesMenu;
    [SerializeField] private Sprite[] pointsSprites;
    public int OwnedCartridgesAmount => OwnedCartridges.Count;
    private SpriteRenderer[] _pointsHolders;
    private SoundsManager _soundsManager;
    
    //To everyone who is reading this monstrosity
    //I am sincerely sorry but I have no time and I have to repair
    //a problem which causes player to get multiple cartridges
    //I dont know why. It just happens
    private float _timeFromLastCartridge = 0;
    private void Start()
    {
        _soundsManager = FindObjectOfType<SoundsManager>();
        OwnedCartridges = new List<CartridgeType>();
        _gottenCartridgesMenu = GameObject.Find("GottenCartridgeMenu");
        _pointsHolders = _gottenCartridgesMenu.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < 5; i++)
        {
            if (i > CartdrigeToGetInLevel - 1)
            {
                _pointsHolders[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        _timeFromLastCartridge += Time.deltaTime; 
        for (int i = 0; i < OwnedCartridges.Count; i++)
        {
            _pointsHolders[i].sprite = pointsSprites[(int) OwnedCartridges[i]];
        }
    }

    public enum CartridgeType
    {
        Red,
        Orange,
        Green,
        LightBlue,
        DarkBlue
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CartridgeObject>() && _timeFromLastCartridge > 0.1f)
        {
            _timeFromLastCartridge = 0;

            OwnedCartridges.Add(other.GetComponent<CartridgeObject>().Type);
            Destroy(other.gameObject);
            if (_soundsManager != null)
            {
                _soundsManager.PlaySound(3);
            }

            if (OwnedCartridgesAmount >= CartdrigeToGetInLevel)
            {
                LevelManager.LoadNextLevel();
            }
        }
    }
}