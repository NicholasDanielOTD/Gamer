using MyGame;
using System;
using System.Collections.Generic;

public static class Pathfinding
{

    public class Path {
        public List<Tile> path = new List<Tile>();
        public Tile goal;
    }

    public static double DistanceBetweenTwoTiles(Tile a, Tile b)
    {
        double distx = Math.Abs(a.pos.X - b.pos.X);
        double disty = Math.Abs(a.pos.Y - b.pos.Y);
        return distx + disty;
    }

    private static Path Reconstruct(Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        Path totalPath = new Path();
        totalPath.goal = current;
        totalPath.path.Add(current);
        while(cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.path.Insert(0, current);
        }
        return totalPath;
    }

    public static Path AStar(Tile start, Tile goal, World world)
    {
        Path openSet = new Path();
        openSet.path.Add(start);
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
        Dictionary<Tile, double> gScore = new Dictionary<Tile, double>();
        gScore.Add(start, 0);
        Dictionary<Tile, double> fScore = new Dictionary<Tile, double>();
        fScore.Add(start, DistanceBetweenTwoTiles(start, goal));

        while (openSet.path.Count > 0)
        {
            Tile current = openSet.path[0];
            double lowest = Int16.MaxValue;
            foreach (Tile tile in openSet.path) if (fScore[tile] < lowest) { current = tile; lowest = fScore[tile]; }
            if (current == goal) return Reconstruct(cameFrom, current);
            openSet.path.Remove(current);

            Tile[] neighbors = current.GetNeighbors(world);
            foreach (Tile neighbor in neighbors)
            {
                if (neighbor == null || !neighbor.CanBeMovedTo()) continue;

                if (!fScore.ContainsKey(neighbor))
                {
                    cameFrom[neighbor] = current;
                    fScore[neighbor] = fScore[current] + 1 + DistanceBetweenTwoTiles(neighbor, goal);
                    if (!openSet.path.Contains(neighbor)) openSet.path.Add(neighbor);
                }
            }
        }

        return new Path();
    }


}