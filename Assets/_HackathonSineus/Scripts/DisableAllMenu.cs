using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menus = new List<GameObject>();
    [SerializeField] private GameObject _mainMenu;

    void Start()
    {
        foreach (var menu in _menus)
        {
            menu.SetActive(false);
        }
        _mainMenu.SetActive(true);
    }
}
