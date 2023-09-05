namespace RPGame.Controller
{
    interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController controller);
    }
}
