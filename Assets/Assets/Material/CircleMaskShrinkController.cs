using System.Collections;
using UnityEngine;

public class CircleMaskShrinkController : MonoBehaviour
{
    [Header("Material & Shrink Settings")]
    [SerializeField] private Material circleMaskMaterial;
    [SerializeField] private Vector2 center = new Vector2(0.5f, 0.5f);
    [SerializeField] private float startRadius = 1f;
    [SerializeField] private float endRadius = 0.1f;
    [SerializeField] private float shrinkSpeed = 0.5f;

    private float currentRadius;

    private void Start()
    {
        // 초기 설정
        currentRadius = startRadius;
        ApplyMaterial();
    }

    private void ApplyMaterial()
    {
        circleMaskMaterial.SetVector("_Center", center);
        circleMaskMaterial.SetFloat("_Radius", currentRadius);
        circleMaskMaterial.SetFloat("_Aspect", (float)Screen.width / Screen.height);
    }

    // 외부에서 코루틴으로 호출
    public IEnumerator ShrinkRoutine()
    {
        currentRadius = startRadius;
        ApplyMaterial();

        float aspect = (float)Screen.width / Screen.height;

        while (currentRadius > endRadius)
        {
            currentRadius = Mathf.MoveTowards(currentRadius, endRadius, shrinkSpeed * Time.deltaTime);
            circleMaskMaterial.SetFloat("_Radius", currentRadius);
            circleMaskMaterial.SetFloat("_Aspect", aspect);
            yield return null;
        }
    }

    // 실행 전에 Center 위치 Scene 뷰에서 미리 확인 (선택적)
    private void OnDrawGizmos()
    {
        // 카메라 크기 기준 → 월드 좌표 변환
        Camera cam = Camera.main;
        if (cam == null) return;

        // UV(0~1) 좌표 → 뷰포트 좌표 → 월드 좌표 변환
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(center.x, center.y, cam.nearClipPlane + 1f));

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(worldPos, 0.1f); // 중심점 표시

        // 큰 원 반경 미리보기 (startRadius 기준)
        float radiusWorld = cam.orthographicSize * startRadius;
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawWireSphere(worldPos, radiusWorld);

        // 작은 원 반경 미리보기 (endRadius 기준)
        float radiusMiniWorld = cam.orthographicSize * endRadius;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPos, radiusMiniWorld);
    }
}