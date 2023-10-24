using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float MaxSpeed;
    public float AccelerationTime;

    public float ParallaxCoef;

    //public Animator anim;

    [SerializeField] private Parallax _foreground;
    [SerializeField] private Parallax _layer1;
    [SerializeField] private Parallax _layer2;
    [SerializeField] private Parallax _layer3;

    private float _currentSpeed = 0;
    private float _startTime;
    private float _stopTime;
    private bool? _isStarted = null;
    private float _speedCoef;

    private void Start()
    {
        StartMovement();
    }

    private void Update()
    {
        if (_isStarted != null)
        {
            if (_isStarted == true)
            {
                if ((Time.time - _startTime) < AccelerationTime) _currentSpeed = (Time.time - _startTime) * (MaxSpeed / AccelerationTime);
                else _currentSpeed = MaxSpeed;
            }
            else _currentSpeed = (_stopTime - Time.time + AccelerationTime) * (MaxSpeed / AccelerationTime);
            if (_currentSpeed < 0) _currentSpeed = 0;
        }
        else _currentSpeed = 0;
        _speedCoef = _currentSpeed / MaxSpeed;
        //anim.speed = _speedCoef;
        _foreground.Speed = _currentSpeed;
        _layer1.Speed = _currentSpeed * ParallaxCoef;
        _layer2.Speed = _currentSpeed * ParallaxCoef * ParallaxCoef; ;
        _layer3.Speed = _currentSpeed * ParallaxCoef * ParallaxCoef * ParallaxCoef;
    }

    public void StartMovement()
    {
        _isStarted = true;
        _startTime = Time.time;
    }

    public void StopMovement()
    {
        _isStarted = false;
        _stopTime = Time.time;
    }
}
