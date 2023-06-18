using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarnDynamicTask
{
    public class Barn
    {
        private int _mapLength;
        private bool[,] _map;
        private int[] _heights;
        private int[] _rightIndexes;
        private int[] _leftIndexes;

        public Barn(bool[,] map)
        {
            this._map = map;
            _mapLength= map.GetLength(0);
            _heights = new int[_mapLength];
            _leftIndexes = new int[_mapLength];
            _rightIndexes = new int[_mapLength];
        }
        public int SolveN4()
        {
            var maxSquare = 0;
            for(int y = 0; y < _mapLength; y++)
            {
                for(int x = 0; x < _mapLength; x++)
                {
                    var square = FindSquareN4(x, y, maxSquare);
                    if(maxSquare < square)
                    {
                        maxSquare = square;
                    }
                }
            }
            return maxSquare;
        }
        public int SolveN3()
        {
            var maxSquare = 0;
            for (int y = 0; y < _mapLength; y++)
            {
                CalculateHeights(y);
                for (int x = 0; x < _mapLength; x++)
                {
                    var square = FindSquareN3(x, y, maxSquare);
                    if (maxSquare < square)
                    {
                        maxSquare = square;
                    }
                }
            }
            return maxSquare;
        }
        public int SolveN2()
        {
            var maxSquare = 0;
            for (int y = 0; y < _mapLength; y++)
            {
                CalculateHeights(y);
                CalculateLeftIndexes(y);
                CalculateRightIndexes(y);
                for (int x = 0; x < _mapLength; x++)
                {
                    var height = _heights[x];
                    var width = _rightIndexes[x] - _leftIndexes[x] + 1;
                    var square = width * height;
                    if (maxSquare < square)
                    {
                        maxSquare = square;
                    }
                }
            }
            return maxSquare;
        }

        private void CalculateHeights(int y)
        {
            for(int x = 0; x < _mapLength; x++)
            {
                if (_map[x,y])
                {
                    _heights[x] = 0;
                }
                else
                {
                    _heights[x]++;
                }
            }
        }
        private int FindSquareN4(int x, int y, int maxSquare)
        {
            var limHeigth = _mapLength;
            for(int width = 1; x + width - 1 < _mapLength; width++)
            {
                var heigth =  FindHeigth(x + width - 1, y, limHeigth);
                if(limHeigth > heigth)
                    limHeigth = heigth;

                if (limHeigth * (_mapLength - x) < maxSquare)
                    break;
                var square = width * limHeigth;

                if(maxSquare < square)
                    maxSquare = square;

            }
            return maxSquare;
        }
        private int FindSquareN3(int x, int y, int maxSquare)
        {
            var limHeigth = _mapLength;
            for (int width = 1; x + width - 1 < _mapLength; width++)
            {
                var heigth = _heights[x + width - 1];
                if (limHeigth > heigth)
                    limHeigth = heigth;

                if (limHeigth * (_mapLength - x) < maxSquare)
                    break;
                var square = width * limHeigth;

                if (maxSquare < square)
                    maxSquare = square;

            }
            return maxSquare;
        }
        private int FindHeigth(int x, int y, int limHeigth)
        {
            var heigth = 0;
            while(heigth <= limHeigth && y - heigth >= 0 && !_map[x, y - heigth])
                heigth++;
            return heigth;
        }
        private void CalculateLeftIndexes(int y)
        {
            var stack = new Stack<int>();
            for (int x = _mapLength - 1; x >= 0; x--)
            {
                while (stack.Count != 0)
                {
                    if (_heights[stack.Peek()] > _heights[x])
                    {
                        _leftIndexes[stack.Pop()] = x + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                stack.Push(x);
            }
            while (stack.Count() != 0)
                _leftIndexes[stack.Pop()] = 0;
        }
        private void CalculateRightIndexes(int y)
        {
            var stack = new Stack<int>();
            for (int x = 0; x < _mapLength; x++)
            {
                while(stack.Count != 0)
                {
                    if (_heights[stack.Peek()] > _heights[x])
                    {
                        _rightIndexes[stack.Pop()] = x - 1;
                    }
                    else
                    {
                        break;
                    }
                }
                stack.Push(x);
            }
            while (stack.Count() != 0)
                _rightIndexes[stack.Pop()] = _mapLength - 1;
        }
    }
}
