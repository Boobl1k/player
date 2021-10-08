namespace backend.Database
{
    // public class User : IDatabasable
    // {
    //     public int Id { get; set; /*private set?*/ }
    //
    //     public string Nickname
    //     {
    //         get => Nickname;
    //         set
    //         {
    //             Nickname = value;
    //             //this.Update(); //
    //         }
    //     }
    //
    //     public User(int id, string nickname)
    //     {
    //         Id = id;
    //         Nickname = nickname;
    //     }
    // }
    //На будущее если архитектура будет требовать, надо отредачить ибо наколеньках написал, а пока использую record
    
    public record User(int Id, string Nickname, string Password) : IDatabase;
    //records являются неизменяемыми
    //Так сказать поиграем в игру: нет права на ошибку
}