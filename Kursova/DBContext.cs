using System.Collections.Generic;

//Класс який імітує взаємодію додатку з базою даних. Замість бд використані просто списки

public class DBContext
{
    public List<User> Users { get; set; } = new List<User>();//Список усіх юзерів
    public List<Game> Games { get; set; } = new List<Game>();//Список усіх ігор
}