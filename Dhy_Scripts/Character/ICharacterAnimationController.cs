using MxM;

public enum EventDefinitionType
{
    PickUp,
    Hammer,
    ItemChange
}
public interface ICharacterAnimationController : ICharacterModule
{
    MxMAnimator mxMAnimator { get; }
    void BeginEventing(EventDefinitionType type);
    void BeginOpenBackPack();
    void EndBackPack();
    void BeginRifling();
    void ExitRifling();
}
