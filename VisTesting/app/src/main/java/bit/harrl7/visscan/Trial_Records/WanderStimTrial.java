package bit.harrl7.visscan.Trial_Records;

import android.graphics.Point;

/**
 * Created by Liam on 02-May-17.
 */

public class WanderStimTrial
{
    public Point pos;
    public boolean hit;

    public WanderStimTrial(Point pos, boolean hit)
    {
        this.pos = pos;
        this.hit = hit;
    }

    public String ToCsv()
    {
        return pos.x + "," + pos.y + "," + hit;
    }
}