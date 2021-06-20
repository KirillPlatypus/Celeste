namespace UI.Button
{
    public interface IButton
    {
        ButtonCode buttonCode{get;}

        bool pointerDown{get; set;}

        bool pointer{get; set;}

    }
}