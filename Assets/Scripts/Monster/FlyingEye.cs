namespace Knight.Adventure
{
    /**
     * 특징
     *  기동성 특화
     *  빨리 다가와서 자주 공격하지만 한 방은 약한 몬스터
     *  플레이어를 압박하는 역할
     *
     * hp: 10f (체력 낮음)
     * speed: 3f (빠름)
     * attackTime: 1.5f (공격 텀 짧음)
     * damage: 1f (데미지 낮음)
     * traceDistance: 7f (먼 거리에서도 추적)
     * attackDistance: 1.5f (근접 공격 or 돌진)
     */
    public class FlyingEye : BaseMonster
    {
        protected override void Start()
        {
            Init(
                10f, 3f, 1.5f, 1f,
                7f, 1.5f, 5);
        }
    }
}