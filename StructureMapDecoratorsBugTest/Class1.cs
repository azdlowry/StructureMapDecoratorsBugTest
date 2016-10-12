using System;
using StructureMap;

namespace StructureMapDecoratorsBugTest
{
    public interface IDoThings
    {
        void DoThing();
    }

    public class ThingDoer : IDoThings
    {
        public void DoThing()
        {
            Console.WriteLine("Do that thing");
        }
    }

    public class DoThingsDecorator : IDoThings
    {
        private readonly IDoThings thingDoer;

        public DoThingsDecorator(IDoThings thingDoer)
        {
            this.thingDoer = thingDoer;
        }

        public void DoThing()
        {
            Console.WriteLine("Decorate that thing");

            thingDoer.DoThing();
        }
    }

    public class Program
    {
        public static void Main()
        {
            var container = new Container(_ =>
            {
                _.For<IDoThings>().Use<ThingDoer>();
            });

            using (var nestedContainer = container.GetNestedContainer())
            {
                nestedContainer.Configure(_ =>
                {
                    _.For<IDoThings>().DecorateAllWith<DoThingsDecorator>();
                });

                nestedContainer.GetInstance<IDoThings>().DoThing();
            }

            container.GetInstance<IDoThings>().DoThing();

            Console.ReadKey();
        }
    }
}
