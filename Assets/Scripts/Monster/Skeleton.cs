namespace Knight.Adventure
{
    /**
     * 특징
     *  공격력 특화
     *  잘 맞으면 금방 죽지만, 플레이어가 방심하면 큰 피해를 입히는 고위험 몬스터
     *
     * hp:              15f (낮음)
     * speed:           1.5f (보통)
     * attackTime:      1.8f (빠른 편)
     * damage:          4f (강한 공격력)
     * traceDistance:   6f
     * attackDistance:  1.3f
     * gainExp:         15
     */
    public class Skeleton : BaseMonster
    {
        protected override void Awake()
        {
            Init(
                15f, 1.5f, 1.8f, 4f,
                6f, 1.3f, 15);
        }
    }
}