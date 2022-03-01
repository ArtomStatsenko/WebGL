public sealed class Idle : State
{
    public override void Handle(PlayerAnimator context)
    {
        if (context.IsJumping)
        {
            context.State = new Jump();
        }
        else
        {
            context.State = new Walk();
        }
    }

}

public class Walk : State
{
    public override void Handle(PlayerAnimator context)
    {
        if (context.IsJumping)
        {
            context.State = new Jump();
        }
        else
        {
            context.State = new Idle();
        }
    }
}

public class Jump : State
{
    public override void Handle(PlayerAnimator context)
    {
        context.State = new Idle();
    }
}