using Localization;

namespace Services.MessageService
{
    public class MessageService
    {
        private readonly MessageView view;
        private readonly LocalizationService localization;
        
        public MessageService(MessageView messageView, LocalizationService localizationService)
        {
            view = messageView;
            localization = localizationService;
        }

        public void Send(string message)
        {
            var translated = localization.GetText(message);
            view.Show(translated);
        }
    }
}