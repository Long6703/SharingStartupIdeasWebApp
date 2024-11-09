namespace SSI.Ultils.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string? Displayname { get; set; }
        public string Email { get; set; } = null!;
        public string? Role { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? Profession { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
    }
}
