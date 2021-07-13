using System;
using System.Collections.Generic;

namespace HwProjTask
{
    class Entity1
    {
        public Entity1(int entityId, string data, DateTime creationTime, int? entity2Id, bool flag)
        {
            EntityId = entityId;
            Data = data;
            CreationTime = creationTime;
            Entity2Id = entity2Id;
            Flag = flag;
        }

        public int EntityId { get; set; }
        public string Data { get; set; }
        public DateTime CreationTime { get; set; }
        public int? Entity2Id { get; set; }
        public bool Flag { get; set; }
    }

    class Entity2
    {
        public Entity2(int entityId, double data, DateTime creationDate)
        {
            EntityId = entityId;
            Data = data;
            CreationDate = creationDate;
        }

        public int EntityId { get; set; }
        public double Data { get; set; }
        public DateTime CreationDate { get; set; }
    }

    class EntityAggregate
    {
        public EntityAggregate(Entity1 e1, Entity2 e2)
        {
            EntityId = e1.EntityId;
            Data = e1.Data;
            CreationTime = e1.CreationTime;
            Flag = e1.Flag;
            Entity2 = e2;
        }

        public int EntityId { get; set; }

        public string Data { get; set; }

        public DateTime CreationTime { get; set; }

        public bool Flag { get; set; }

        public Entity2 Entity2 { get; set; }
    }


    class Program
    {
        /// Задача:
        /// Даны два массива сущностей,
        /// Сущности из первого массива содержат идентификаторы сущностей из второго массива,
        /// Реализовать Join, обьединяющий данные сущностей из двух массивов,
        /// Вернуть массив EntityAggregate, содержащий обьединённые данные,
        /// предварительно отфильтровав сущности из первого массива по entity.Flag == hasFlag
        ///
        /// Пример работы:
        ///
        /// Входные данные:
        /// e1 = Entity1(EntityId = 1,
        ///              Data = "123",
        ///              Flag = false,
        ///              Entity2Id = 2,
        ///              CreationDate = 29.10.1999)
        ///
        /// e2 = Entity2(EntityId = 2,
        ///              Data = 2.4,
        ///              CreationDate = 30.10.1999)
        ///
        /// hasFlag = false
        ///
        /// Возвращаемое значение:
        /// EntityAggregate(EntityId = 1,
        ///                 Data = "123",
        ///                 Flag = false,
        ///                 CreationDate = 29.10.1999,
        ///                 Entity2 = Entity2(EntityId = 2,
        ///                                   Data = 2.4,
        ///                                   CreationDate = 30.10.1999)
        static EntityAggregate[] Join(Entity1[] e1, Entity2[] e2, bool hasFlag)
        {
            var list = new List<EntityAggregate>();
            foreach (var entity1 in e1)
            {
                if (entity1.Flag == hasFlag)
                {
                    if (entity1.Entity2Id == null)
                    {
                        list.Add(new EntityAggregate(entity1, null));
                    }
                    else
                    {
                        foreach (var entity2 in e2)
                        {
                            if (entity2.EntityId == entity1.Entity2Id)
                            {
                                list.Add(new EntityAggregate(entity1, entity2));
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        static void Main(string[] args)
        {
            var e1 = new Entity1[1];
            e1[0] = new Entity1(1, "123", new DateTime(1999, 10, 29), 2, false);
            var e2 = new Entity2[1];
            e2[0] = new Entity2(2, 2.4, new DateTime(1999, 10, 30));
            var entityAggregate = Join(e1, e2, false);
        }
    }
}
