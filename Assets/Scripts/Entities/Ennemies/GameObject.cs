using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class GameObject : MonoBehaviour
{
    /*LE problème que je rencontre avec le component pattern par rapport à l'héritage, c'est que vu que les comportements spécifiques sont instanciés après le
     début de la scène, je ne peux rien accrocher dessus via des champs sérialisés.
    Ducoup, tout objet qui doit être utilisé par un Strategy doit soit être récupéré à la main par chaque script lui même, soit récupéré par le wave manager
    et au script strategy lors de awake. 
    Au contraire on pourrait faire un script archer qui hérite de ennemy et qui execute les awake et update de enemy, mais a ses propres champs.
    Ah en vrai ça résoud pas le pb de sérialisation j'ai l'impression, vu qu'on peut quand même pas attacher d'objets de scène à un objet dans les dossiers
    Honnetement je m'y perd un peu*/

    //We use EnemyType to instanciate Move and Attack types rather than attaching scripts directly
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyParameter enemyData;
    private float cdTimer = 0f;
    private Strategy strategy;

    [HideInInspector]
    public UnityEngine.GameObject Player { get; set; }
    [HideInInspector]
    public Camera Camera { get; set; }

    NavMeshAgent agent;
    Animator animator;
    [SerializeField] UnityEngine.GameObject enemyModel;
    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        agent.speed = enemyData.speed;
        agent.acceleration = 10 * enemyData.speed;
    }

    private void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Warrior:
                //strategy = gameObject.AddComponent<*nom de votre script de stratégie*>();
                break;
            case EnemyType.Archer:
                strategy = gameObject.AddComponent<ArcherStrategy>();
                strategy.Target = Player.transform;
                strategy.Camera = Camera;



                break;
            case EnemyType.Liche:
                //strategy = gameObject.AddComponent<*nom de votre script de stratégie*>();
                break;
            default:
                break;
        }
    }

    public void SetTarget(UnityEngine.GameObject target)
    {
        strategy.Target = target.transform;
        //strategy.SetTarget(target.transform);
    }

    void Update()
    {
        //peut être gérer de manière différente, par exemple avoir une liste des CD en cours, et des cd à 0 (ie des action qui doivent être exécutées
        //ainsi on peut simplement update les cdTimer qui ne sont pas finis, et checker les autre, ça passe la complexité de n2 à n mais la différence sera pas visible ici je pense
        cdTimer += Time.deltaTime;
        if(cdTimer >= enemyData.attackCD)
        {
            if (strategy.Attack())
            {
                cdTimer = 0;
            }
        }
        agent.destination = strategy.Move();
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
