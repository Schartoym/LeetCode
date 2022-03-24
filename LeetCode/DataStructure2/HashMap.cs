using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// Design a HashMap without using any built-in hash table libraries.
/// </summary>
/// <remarks>
/// Constraints:
///   0 <= key, value <= 106
///   At most 10^4 calls will be made to put, get, and remove.
///</remarks>
public class HashMap
{
    private readonly Random _random = new();

    [Fact]
    public void Add_Value_Twice_Then_Get_Values()
    {
        var map = new MyHashMap();
        var key1 = _random.Next() + 1;
        var key2 = _random.Next() + 1;
        
        map.Put(key1, 100);
        map.Put(key2, 200);
        
        map.Get(key1).Should().Be(100);
        map.Get(key2).Should().Be(200);
    }
    
    [Fact]
    public void Adding_Value_With_Same_Key_Replaces_Value()
    {
        var map = new MyHashMap();
        var key = _random.Next() + 1;
        
        map.Put(key, 100);
        map.Get(key).Should().Be(100);

        map.Put(key, 200);
        map.Get(key).Should().Be(200);
    }

    [Fact]
    public void Add_Then_Remove_Returns_MinusOne()
    {
        var map = new MyHashMap();
        var key = _random.Next() + 1;

        map.Put(key, 100);
        map.Get(key).Should().Be(100);

        map.Remove(key);
        map.Get(key).Should().Be(-1);
    }
    
    [Fact]
    public void Get_Non_Exists_Key_Returns_Minus_One()
    {
        var map = new MyHashMap();
        var key = _random.Next() + 1;
        
        map.Get(key).Should().Be(-1);
    }
    
    [Fact]
    public void Add_Key_With_Collision_Then_Get_Values()
    {
        var map = new MyHashMap();
        var key1 = MyHashMap.HashSize;
        var key2 = 2 * MyHashMap.HashSize;
        
        map.Put(key1, 100);
        map.Put(key2, 200);
        
        map.Get(key1).Should().Be(100);
        map.Get(key2).Should().Be(200);
    }
    
    public class MyHashMap
    {
        //Using prime number for hashing function
        public const int HashSize = 997;
        
        private readonly LinkedList<Node>[] _hashTable = new LinkedList<Node>[HashSize];
        public MyHashMap() 
        {
            for (int i = 0; i < HashSize; i++)
            {
                _hashTable[i] = new LinkedList<Node>();
            }
        }
    
        public void Put(int key, int value)
        {
            var (nodeWithKey, list) = GetNode(key);

            if (nodeWithKey == null)
            {
                nodeWithKey = new Node
                {
                    Key = key,
                    Value = value
                };

                list.AddLast(nodeWithKey);
            }
            else
            {
                nodeWithKey.Value = value;
                nodeWithKey.IsDeleted = false;
            }
        }

        private (Node? nodeWithKey, LinkedList<Node> list) GetNode(int key)
        {
            var index = GetHashIndex(key);
            var list = _hashTable[index];

            var nodeWithKey = list.FirstOrDefault(node => node.Key == key);

            return (nodeWithKey, list);
        }

        public int Get(int key)
        {
            var (node, _) = GetNode(key);

            if (node == null || node.IsDeleted)
            {
                return -1;
            }

            return node.Value;
        }
    
        public void Remove(int key) 
        {
            var (node, _) = GetNode(key);

            if (node != null)
            {
                node.IsDeleted = true;
            }
        }
        
        private class Node
        {
            public bool IsDeleted { get; set; }

            public int Value { get; set; }
            
            public int Key { get; set; }
        }

        private int GetHashIndex(int value)
        {
            return value % HashSize;
        }
    }
}