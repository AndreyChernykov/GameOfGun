using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] float sensitivityRotation;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioShoot;
    [SerializeField] AudioClip audioCharge;
    [SerializeField] int ammoInGun;
    [SerializeField] Transform shotPoint;   
    [SerializeField] GameObject bullet;

    List<GameObject> bulletsPool = new List<GameObject>();
    List<int>levelGun = new List<int>();

    StatePlayer state;

    (float, float) clampVertRotation = (-35, 35);
    (float, float) clampPitchAudio = (0.8f, 1.2f);

    bool isShooting = false;
    float timeToReload = 2.2f;
    float rotationX;
    int shotCount = 0;
    
    string animationInteger = "StatePlayer";

    public int playerLevel { get; private set; }
    public int points { get; private set; }

    enum StatePlayer 
    {
        isIdle,
        isShoot,
        isReload,
    }

    private void Awake()
    {
        EventManager.events.AddListener(AddPoints);
    }

    private void Start()
    {
        levelGun = playerData.playerLevel;
        playerLevel = 1;
        FillBulletPool();
    }

    private void FillBulletPool()
    {
        for (int i = 0; i < ammoInGun; i++)
        {
            GameObject bl = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            bl.SetActive(false);
            bulletsPool.Add(bl);
        }
    }

    public void Rotation(float inputY, float inputX)
    {
        rotationX += inputY * sensitivityRotation;
        rotationX = Mathf.Clamp(rotationX, clampVertRotation.Item1, clampVertRotation.Item2);

        float delta = inputX * sensitivityRotation;
        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }

    public void Shoot()
    {

        if (shotCount >= ammoInGun)
        {
            shotCount = 0;
            state = StatePlayer.isReload;
            audioSource.pitch = Random.Range(clampPitchAudio.Item1, clampPitchAudio.Item2);
            audioSource.PlayOneShot(audioCharge);
            isShooting = true;
            Invoke("Reload", timeToReload);
        }
        else
        {
            if (!isShooting)
            {
                GetFromPool();
                shotCount++;
                audioSource.pitch = 1;
                audioSource.PlayOneShot(audioShoot);
                state = StatePlayer.isShoot;
            }

        }
        StartCoroutine(ShootAnimation(state));       
    }

    private void Reload() => isShooting = false;

    private IEnumerator ShootAnimation(StatePlayer statePlayer)
    {
        animator.SetInteger(animationInteger, (int)statePlayer);
        yield return new WaitForFixedUpdate();        
        animator.SetInteger(animationInteger, (int)StatePlayer.isIdle);
    }

    private void GetFromPool()
    {
        foreach (var bullet in bulletsPool)
        {
            if (!bullet.activeSelf)
            {
                bullet.transform.position = shotPoint.position;
                bullet.transform.rotation = shotPoint.rotation;
                bullet.GetComponent<Bullet>().SetDamage(playerLevel);
                bullet.SetActive(true);
                break;
            }
        }

    }

    void AddPoints(int p)
    {
        points += p;
        Debug.Log("points = " + points);
        UpdateGun();
    }

    private void UpdateGun()
    {
        for (int i = playerLevel; i < levelGun.Count; i++)
        {
            if (points >= levelGun[i]) playerLevel = i;
        }
    }
}
