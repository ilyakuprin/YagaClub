using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidesSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _slides = new List<GameObject>();
    [SerializeField] private GameObject _slidesCanvas;

    private int _curSlide = 0;

    private void Start()
    {
        foreach (var slide in _slides)
        {
            slide.SetActive(false);
        }

        _slides[0].SetActive(true);
    }

    public void SwithSlide()
    {
        _slides[_curSlide].SetActive(false);

        _curSlide++;
        if (_curSlide == _slides.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            _slides[_curSlide].SetActive(true);
        }
    }
}
