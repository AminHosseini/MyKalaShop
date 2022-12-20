namespace DiscountManagement.Infrastructure.Configuration.Permissions
{
    public static class DiscountPermissions
    {
        public const int AccessToDiscountManagement = 200;

        public const int ListCustomerDiscounts = 201;
        public const int SearchCustomerDiscounts = 202;
        public const int CreateCustomerDiscount = 203;
        public const int EditCustomerDiscount = 204;

        public const int ListColleagueDiscounts = 210;
        public const int SearchColleagueDiscounts = 211;
        public const int CreateColleagueDiscount = 212;
        public const int EditColleagueDiscount = 213;
    }
}
