namespace HReception.Logic.Services.Interfaces.Payment
{
    public class NewTransactionReponse
    {
        public NewTransactionResult Result { get; set; }
    }

    public enum NewTransactionResult
    {
        Failed,
        Succeeded
    }
}