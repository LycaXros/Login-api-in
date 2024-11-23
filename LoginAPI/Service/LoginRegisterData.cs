namespace LoginAPI.Service
{
    public record LoginRegisterData(string Name, string Password, string Email, PhoneRegister[] Phones);
}