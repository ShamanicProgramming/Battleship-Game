namespace BattleshipGame.Util
{
    class MessageHandler
    {
        private Action<string> messageAction;
        private Action clearMessagesAction;

        public MessageHandler(Action<string> uiMessageAction, Action uiClearMessagesAction) 
        {
            messageAction = uiMessageAction;
            clearMessagesAction = uiClearMessagesAction;
        }

        public void PushMessage(string message)
        {
            messageAction.Invoke(message + "\n");
        }

        internal void ClearMessages()
        {
            clearMessagesAction.Invoke();
        }
    }
}
