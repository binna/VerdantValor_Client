namespace Knight.Adventure
{
    /**
     * 특징
     *  느리지만 단단함
     *  단단해서 쉽게 안 죽음
     *  천천히 다가오지만 오래 살아남아 플레이어 동선을 막음
     *
     * hp:              35f (높음)
     * speed:           0.8f (느림)
     * attackTime:      3f (공격 텀 길음)
     * damage:          2f (중간)
     * traceDistance:   4f (짧음)
     * attackDistance:  1f
     * gainExp:         20
     */
    public class Mushroom : BaseMonster
    {
        
        protected override void Awake()
        {
            Init(
                35f, 0.8f, 3f, 2f,
                4f, 1f, 20);
        }
    }
}