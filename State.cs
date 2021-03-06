﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{


	public struct Position
	{
		public byte X;
		public byte Y;

		public Position(byte x, byte y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return "{" + X + ", " + Y + "}";
		}
	}

	public class State
	{
		public enum Direction
		{
			Up,
			Down,
			Left,
			Right
		}

		public const char Agent = '☺';

		public char[] Grid;

        public byte Size;

		public Position AgentLocation
		{
			get
			{
				int index = Array.FindIndex(Grid, c => c == State.Agent);
				return new Position { X = (byte)(index % Size), Y = (byte)(index / Size) };
			}
		}

		public State(byte size, char[] grid)
		{
            Size = size;
			Grid = grid;
		}

		public State MakeMove(Direction direction)
		{
			Position agentLocation = this.AgentLocation;
			Position newAgentLocation = new Position();

            //moves that would take us off the grid are null and ignored
			switch(direction)
			{
				case Direction.Up:
					if (agentLocation.Y == 0)
						return null;
					else
						newAgentLocation = new Position(agentLocation.X, (byte)(agentLocation.Y - 1));
					break;
				case Direction.Down:
					if (agentLocation.Y == Size - 1)
						return null;
					else
						newAgentLocation = new Position(agentLocation.X, (byte)(agentLocation.Y + 1));
					break;
				case Direction.Right:
					if (agentLocation.X == Size - 1)
						return null;
					else
						newAgentLocation = new Position((byte)(agentLocation.X + 1), agentLocation.Y);
					break;
				case Direction.Left:
					if (agentLocation.X == 0)
						return null;
					else
						newAgentLocation = new Position((byte)(agentLocation.X - 1), agentLocation.Y);
					break;
			}

			char[] newGrid = (char[])Grid.Clone();
			char displacedSymbol = newGrid[newAgentLocation.Y * Size + newAgentLocation.X];
			newGrid[agentLocation.Y * Size + agentLocation.X] = displacedSymbol;
			newGrid[newAgentLocation.Y * Size + newAgentLocation.X] = State.Agent;
			State newState = new State(Size, newGrid);
			return newState;
		}

		public State[] GetAllMoves()
		{
			State[] validStates = new[] { MakeMove(Direction.Up), MakeMove(Direction.Down), MakeMove(Direction.Left), MakeMove(Direction.Right) }.Where(m => m != null).ToArray();
			//Fisher-Yates shuffle
			//If we return as we are, DFS very quickly reaches an infinite loop (go up until agent is at the top, then go down-up infinitely)
			for (int i = validStates.Count() - 1; i > 0; --i)
			{
				byte j = (byte)Program.Random.Next(i+1);
				State tmp = validStates[i];
				validStates[i] = validStates[j];
				validStates[j] = tmp;
			}
				return validStates;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			for (int y = 0; y < Size; ++y)
			{
				builder.Append(String.Join(" ", Grid.Skip(y * Size).Take(Size)));
				builder.Append("\n");
			}
			return builder.ToString();
		}

		public override int GetHashCode()
		{
			return new string(Grid).GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return this.GetHashCode() == obj.GetHashCode();
		}
	}
}
