using UnityEngine;
using UnityEngine.AI;

public class SpaceShipController : EntityController, ILoadingComponent<SpaceShipData>
{
    private NavMeshAgent _agent;
    private TypeClassPrefs _prefs;
    
    public override void Initialize(Entity newOwner)
    {
        owner = newOwner;
        var data = owner.GetData<SpaceShipData>();
        var typeKey = data.spaceShipType.ToString();
        var classKey = data.spaceShipClass.ToString();
        _prefs = G.GetPreferences<SpaceShipPrefs>().GetPreferences(typeKey + classKey);
        owner = newOwner;
        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.angularSpeed = _prefs.baseRotateSpeed;
        _agent.speed = _prefs.baseMoveSpeed;
        G.GetManager<RoutineManager>().StartLateUpdateAction(UpdateAgentRotation);
        Eventer.Subscribe<EmptySpotTargeted>(SetTarget);
    }

    public void Load(SpaceShipData data)
    {
        _agent.Warp(data.position);
        _agent.updateRotation = false;
        transform.rotation = data.rotation;
        _agent.updateRotation = true;
    }
    
    private void SetTarget(EmptySpotTargeted eventData)
    {
        var selector = owner.GetEntityComponent<SpaceShipSelector>();
        if (selector == null) return;
        if (!selector.IsSelected) return;
        var target = eventData.Spot;
        _agent.SetDestination(target);
    }

    private void UpdateAgentRotation()
    {
        if (!_agent.hasPath) return;
        var direction = _agent.steeringTarget - transform.position;
        direction.y = 0;
        direction.Normalize();
        var targetRotation = Quaternion.LookRotation(direction);
        if(Quaternion.Angle(transform.rotation, targetRotation) <= 0.1f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _prefs.baseRotateSpeed * Time.deltaTime);
    }

    public override void Disable() { Eventer.Unsubscribe<EmptySpotTargeted>(SetTarget); }
}