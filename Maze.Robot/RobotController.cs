using Maze.Library;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Maze.Solver
{
    /// <summary>
    /// Moves a robot from its current position towards the exit of the maze
    /// </summary>
    public class RobotController
    {
        private IRobot robot;
        private bool reachedEnd;
        private List<Point> checkedPoints;


        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class
        /// </summary>
        /// <param name="robot">Robot that is controlled</param>
        public RobotController(IRobot robot)
        {
            // Store robot for later use
            reachedEnd = false;
            checkedPoints = new List<Point>();
            this.robot = robot;
        }

        /// <summary>
        /// Moves the robot to the exit
        /// </summary>
        /// <remarks>
        /// This function uses methods of the robot that was passed into this class'
        /// constructor. It has to move the robot until the robot's event
        /// <see cref="IRobot.ReachedExit"/> is fired. If the algorithm finds out that
        /// the exit is not reachable, it has to call <see cref="IRobot.HaltAndCatchFire"/>
        /// and exit.
        /// </remarks>
        public void MoveRobotToExit()
        {
            // Here you have to add your code

            // Trivial sample algorithm that can just move right

            Point start = new Point(0, 0);

            
            robot.ReachedExit += (_, __) => reachedEnd = true;

            this.checkIfPossible(start);


            if (this.reachedEnd == false)
            {
                robot.HaltAndCatchFire();
            }
        }


        public void checkIfPossible(Point p)
        {
            if(this.checkedPoints.Contains(p) == false && reachedEnd == false)
            {
                this.checkedPoints.Add(p);
                if(this.robot.TryMove(Direction.Up) == true &&this.reachedEnd == false)
                {
                    this.checkIfPossible(new Point (p.X, p.Y - 1));
                    checkEnd(Direction.Down);
                }
                if (this.robot.TryMove(Direction.Down) == true && this.reachedEnd == false)
                {
                    this.checkIfPossible(new Point(p.X, p.Y + 1));
                    checkEnd(Direction.Up);
                }
                if (this.robot.TryMove(Direction.Left) == true && this.reachedEnd == false)
                {
                    this.checkIfPossible(new Point(p.X - 1, p.Y));
                    checkEnd(Direction.Right);
                }
                if (this.robot.TryMove(Direction.Right) == true && this.reachedEnd == false)
                {
                    this.checkIfPossible(new Point(p.X + 1, p.Y));
                    checkEnd(Direction.Left);
                }

            }
        }


        public void checkEnd(Direction dir)
        {
            if(this.reachedEnd == false)
            {
                robot.Move(dir);
            }
        }


    }
}
