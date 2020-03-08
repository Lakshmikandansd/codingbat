/* Given two arrays of ints sorted in increasing order, outer and inner, 
 * return true if all of the numbers in inner appear in outer. The best 
 * solution makes only a single "linear" pass of both arrays, taking 
 * advantage of the fact that both arrays are already in sorted order.
 */public boolean linearIn(int[] outer, int[] inner) {
  int count=0;
  for(int i=0;i<inner.length;i++)
  {
    int num=inner[i];
    for(int j=i;j<outer.length;j++)
    {
      if(num==outer[j])
      {
        count++;
        break;
      }
    }
  }
  if(count==inner.length)
  {
  return true;
  }
  return false;
}

