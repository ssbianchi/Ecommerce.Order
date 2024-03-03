namespace Ecommerce.Order.CrossCutting.Exception
{
    public class BusinessException : System.Exception
    {
        public string Title { get; private set; } = "Business exception occured";
        public List<BusinessValidation> Errors { get; private set; } = new List<BusinessValidation>();

        public BusinessException(string title, string busName, string busMessage) : this(title)
        {
            Errors.Add(new BusinessValidation() { Name = busName, Message = busMessage });
        }

        public BusinessException(string title)
        {
            Title = title;
        }

        public void AddError(BusinessValidation error)
        {
            this.Errors.Add(error);
        }

        public void ValidateAndThrow()
        {
            if (this.Errors.Any())
                throw this;
        }
    }

    public class BusinessValidation
    {
        public string Name { get; set; } = "Business error";

        public string Message { get; set; }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
