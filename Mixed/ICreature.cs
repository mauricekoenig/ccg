
using System.Collections.Generic;

public interface ICreature {

    public int Attack { get; set; }
    public int Health { get; set; }
    public List<CreatureType> Types { get; set; }
}
