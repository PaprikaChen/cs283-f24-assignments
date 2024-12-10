using System.Collections;
using UnityEngine;

public class CollectiveLetter : MonoBehaviour
{
    public float animationDuration = 2f; // 动画持续时间
    public Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f); // 缩放目标
    public float rotationAngle = 180; // 旋转角度
    public float height = 3f; // 上升高度
    public float rotationSpeed = 50f; // 每秒旋转速度

    private AudioSource audioSource; // 声音组件
    public LetterUI letterUI; // 引用 LetterUI 脚本
    private bool isBeingCollected = false; // 是否正在被拾取

    public void RunPickupAnimation()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // 播放拾取音效
        }
        isBeingCollected = true; // 标记为正在被拾取
        StartCoroutine(PickupAnimationCoroutine());
    }

    private IEnumerator PickupAnimationCoroutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, height, 0);
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles + new Vector3(0, rotationAngle, 0));

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float progress = elapsedTime / animationDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        transform.localScale = targetScale;
        transform.rotation = endRotation;

        // 更新信件内容并显示新信件
        if (letterUI != null)
        {
            letterUI.UpdateLetterIndex(letterUI.currentLetterIndex + 1);
            letterUI.ToggleLetter();
        }

        // 隐藏对象
        gameObject.SetActive(false);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 如果未被拾取，则持续旋转
        if (!isBeingCollected)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RunPickupAnimation(); // 播放拾取动画
        }
    }
}
