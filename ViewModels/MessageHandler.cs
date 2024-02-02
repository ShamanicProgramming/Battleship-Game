namespace BattleshipGame.ViewModels
{
    class MessageHandler : ViewModelBase
    {
        private Action<string> messageAction;

        public MessageHandler(Action<string> uiMessageAction) 
        {
            messageAction = uiMessageAction;
        }

        public void PushMessage(string message)
        {
            messageAction.Invoke(message + "\n");
        }

    }
}
