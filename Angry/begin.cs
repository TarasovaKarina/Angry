using System;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        StreamReader s = new StreamReader("date.txt");
        Throw t = new Throw(Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()), Double.Parse(s.ReadLine()));

        // Подписываемся на событие
        t.ThrowCollision += HandleCollision;

        t.printThrow();
        s.Close();
    }


    // Обработчик события столкновения
    static void HandleCollision(object sender, CollisionEventArgs e)
    {
        Console.WriteLine($"Столкновение произошло на координатах: ({e.X}, {e.Y})");
    }
}

// Аргументы события столкновения
class CollisionEventArgs : EventArgs
{
    public double X { get; set; }
    public double Y { get; set; }

    public CollisionEventArgs(double x, double y)
    {
        X = x;
        Y = y;
    }
}

class Movement
{
    public event EventHandler<CollisionEventArgs> Collision;
    private double V0;
    private double Alpha0;
    private static double g = 9.81;
    private double h0;
    private double x_col;
    private double obstacleX;
    private double obstacleY;
    private double windSpeed;
    private double windDirection;

    public Movement(double V, double Alpha, double h, double windSpeed, double windDirection, double obstacleX, double obstacleY)
    {
        V0 = V;
        Alpha0 = Alpha;
        h0 = h;
        this.windSpeed = windSpeed;
        this.windDirection = windDirection;
        this.obstacleX = obstacleX;
        this.obstacleY = obstacleY;
    }

    public Movement()
    {
        V0 = 0;
        Alpha0 = 0;
        h0 = 0;
        windSpeed = 0;
        windDirection = 0;
    }

    protected virtual void OnCollision(double x, double y)
    {
        Collision?.Invoke(this, new CollisionEventArgs(x, y));
    }


    public void print()
    {
        Console.WriteLine(imitation(1));
    }

    Tuple<double, double> imitation(double time)
    {
        double x = V0 * Math.Cos(Alpha0) * time;
        double y = h0 + V0 * Math.Sin(Alpha0) - g * time * time / 2;
        x_col = V0 * Math.Cos(Alpha0) * (Math.Sqrt(2 * (V0 * Math.Sin(Alpha0) + h0) / g));
        double xPrev = 0;
        double yPrev = 0;
        double xVel = V0 * Math.Cos(Alpha0);
        double yVel = V0 * Math.Sin(Alpha0);
        bool obstacleHit = false;

        while (y >= 0 && !obstacleHit)
        {
            xPrev = x;
            yPrev = y;
            x += xVel * time;
            y += yVel * time - 0.5 * g * time * time;

            if (x >= obstacleX && y <= obstacleY)
            {
                obstacleHit = true;
                x = obstacleX;
                y = obstacleY;
                OnCollision(x, y);
            }

            double windX = windSpeed * Math.Cos(windDirection);
            double windY = windSpeed * Math.Sin(windDirection);
            xVel = xVel + time * (windX - xVel / (Math.Sqrt(xVel * xVel + yVel * yVel)));
            yVel = yVel + time * (-g + windY - yVel / (Math.Sqrt(xVel * xVel + yVel * yVel)));
        }

        double tCol = time * (Math.Sqrt((xPrev - x_col) * (xPrev - x_col) + (yPrev - 0) * (yPrev - 0))) / (Math.Sqrt((x - xPrev) * (x - xPrev) + (y - yPrev) * (y - yPrev)));
        double xCol = xPrev + (x - xPrev) * tCol;
        double yCol = yPrev + (y - yPrev) * tCol;
        var collision = new Tuple<double, double>(xCol, yCol);
        return collision;
    }

}

class Throw
{
    private Movement movement;
    public event EventHandler<CollisionEventArgs> ThrowCollision; // Событие броска

    public Throw(double V, double Alpha, double h, double windSpeed, double windDirection, double obstacleX, double obstacleY)
    {
        movement = new Movement(V, Alpha, h, windSpeed, windDirection, obstacleX, obstacleY);

        // Подписываемся на событие столкновения
        movement.Collision += (sender, e) => ThrowCollision?.Invoke(this, e);

    }


    // Обработчик события столкновения
    void HandleCollision(object sender, CollisionEventArgs e)
    {
        ThrowCollision?.Invoke(this, e);
    }

    public void printThrow()
    {
        movement.print();
    }

}