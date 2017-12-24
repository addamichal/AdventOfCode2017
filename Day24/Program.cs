using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    internal class Program
    {
        public static List<List<Pin>> bridges = new List<List<Pin>>();

        public static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("input.txt");

            var pins = new List<Pin>();
            foreach (var input in inputs)
            {
                var parts = input.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var pin = new Pin(int.Parse(parts[0]), int.Parse(parts[1]));

                if (pin.Port1 > pin.Port2)
                {
                    pins.Add(new Pin(pin.Port2, pin.Port1));
                }
                else
                {
                    pins.Add(pin);
                }
            }

            pins = pins.OrderBy(w => w.Port1).ThenBy(w => w.Port2).ToList();

            foreach (var pin in pins)
            {
                if (pin.Port1 == 0 || pin.Port2 == 0)
                {
                    Pin newPin = null;
                    if (pin.Port2 == 0)
                    {
                        newPin = new Pin(pin.Port2, pin.Port1);
                    }
                    else
                    {
                        newPin = new Pin(pin.Port1, pin.Port2);
                    }

                    var currentBridge = new List<Pin>() {newPin};
                    BuildBridge(currentBridge, pins);
                }
            }

            int max1 = Int32.MinValue;
            foreach (var bridge in bridges)
            {
                var current = GetBridgeStrength(bridge);

                if (current > max1)
                {
                    max1 = current;
                }
            }

            Console.WriteLine(max1);

            int max2 = Int32.MinValue;
            int maxLength = bridges.Max(w => w.Count);
            foreach (var bridge in bridges.Where(w => w.Count == maxLength).ToList())
            {
                var current = GetBridgeStrength(bridge);

                if (current > max2)
                {
                    max2 = current;
                }
            }

            Console.WriteLine(max2);
        }

        private static int GetBridgeStrength(List<Pin> bridge)
        {
            int current = 0;
            foreach (var pin in bridge)
            {
                current += pin.Port1;
                current += pin.Port2;
            }
            return current;
        }

        public static void BuildBridge(List<Pin> currentBridge, List<Pin> pins)
        {
            foreach (var pin in pins)
            {
                if (!currentBridge.Contains(pin))
                {
                    if (CanAdd(currentBridge, pin))
                    {
                        var bridge = Clone(currentBridge);
                        AddToBridge(bridge, pin);
                        BuildBridge(bridge, pins);
                        bridges.Add(bridge);
                    }
                }
            }
        }

        private static List<Pin> Clone(List<Pin> currentBridge)
        {
            var newBridge = new List<Pin>();
            foreach (var pin in currentBridge)
            {
                newBridge.Add(new Pin(pin.Port1, pin.Port2));
            }
            return newBridge;
        }

        private static bool CanAdd(List<Pin> bridge, Pin pin)
        {
            if (bridge.Count == 0) return true;

            return AreCompabible(bridge.Last(), pin);
        }

        private static bool AreCompabible(Pin pin1, Pin pin2)
        {
            var result = (pin1.Port2 == pin2.Port1) || (pin1.Port2 == pin2.Port2);
            return result;
        }

        public static void AddToBridge(List<Pin> bridge, Pin pin)
        {
            pin = SwitchPinIfNecessary(bridge.Last(), pin);
            bridge.Add(pin);
        }

        private static Pin SwitchPinIfNecessary(Pin pin1, Pin pin2)
        {
            if (pin1.Port2 == pin2.Port1)
            {
                return new Pin(pin2.Port1, pin2.Port2);
            }
            return new Pin(pin2.Port2, pin2.Port1);
        }
    }

    public class Pin : IEquatable<Pin>
    {
        public int Port1 { get; set; }
        public int Port2 { get; set; }

        public Pin(int port1, int port2)
        {
            Port1 = port1;
            Port2 = port2;
        }

        public bool Equals(Pin other)
        {
            var point = this;
            var result = (Port1 == other.Port1 && Port2 == other.Port2) ||
                         (Port2 == other.Port1 && Port1 == other.Port2);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pin) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Port1 * 397) ^ Port2;
            }
        }

        public override string ToString()
        {
            return Port1 + "/" + Port2;
        }
    }
}