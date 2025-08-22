namespace Knight.Adventure
{
    /**
     * 특징
     *  기본 근접 전사
     *  무난하게 강한 기본형
     *  숫자가 많아질수록 위험해지는 타입
     *
     * hp: 20f (중간)
     * speed: 2f (보통)
     * attackTime: 2f (보통 텀)
     * damage: 3f (데미지 꽤 강함)
     * traceDistance: 5f
     * attackDistance: 1.2f
     */
    
    public class Goblin : BaseMonster
    {
        protected override void Start()
        {
            Init(
                20f, 2f, 2f, 3f,
                5f, 1.2f, 12);
        }
    }
}