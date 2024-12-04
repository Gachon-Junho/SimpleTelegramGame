using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDoughnutQueue : MonoBehaviour
{
    public DoughnutInfo Next { get; private set; }
    
    [SerializeField]
    private DoughnutInfo[] Doughnuts;

    [SerializeField] 
    private SpriteRenderer preview;

    private Queue<DoughnutInfo> bag = new Queue<DoughnutInfo>();
    private Queue<DoughnutInfo> pending = new Queue<DoughnutInfo>();

    private void Awake()
    {
        next();
    }

    public Doughnut Get()
    {
        var doughnut = Next.Instantiate();

        next();

        return doughnut;
    }

    private void next()
    {
        if (bag.Count == 0)
            fill();

        Next = bag.Dequeue();

        preview.sprite = Next.Sprite;
    }

    private void fill()
    {
        while (pending.Count > 0)
            bag.Enqueue(pending.Dequeue());

        foreach (var doughnut in shuffle())
            pending.Enqueue(doughnut);

        if (bag.Count == 0)
            fill();
    }
    
    private IEnumerable<DoughnutInfo> shuffle()
    {
        List<DoughnutInfo> bag = new List<DoughnutInfo>();
        Func<int> nextDoughnut = () => Random.Range(0, Doughnuts.Length);

        int count = 0;

        while (count < Doughnuts.Length)
        {
            var doughnutType = nextDoughnut.Invoke();

            // 중복을 허용해서 게임플레이에 불편함이 생기지 않도록 함
            // if (bag.Contains(Doughnuts[doughnutType]))
            //     continue;

            bag.Add(Doughnuts[doughnutType]);
            count++;
        }

        return bag;
    }
}
