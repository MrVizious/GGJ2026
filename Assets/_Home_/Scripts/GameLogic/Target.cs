using UnityEngine;

public class Target : MonoBehaviour
{
    private int idx = -1;
    
    public void setIndex(int idx)
    {
        this.idx = idx;
    }
    
    public int getIndex()
    {
        return idx;
    }
}
