
import java.util.Hashtable;
public class KWTable {

	private Hashtable mTable;
	public KWTable()
	{
		// Inicijalizcaija hash tabele koja pamti kljucne reci
		mTable = new Hashtable();
		
		mTable.put("program", new Integer(sym.PROGRAM));
		mTable.put("begin", new Integer(sym.BEGIN));
		mTable.put("end", new Integer(sym.END));
		mTable.put("epsilon", new Integer(sym.EPSILON));
		mTable.put("integer", new Integer(sym.INTEGER));
		mTable.put("char", new Integer(sym.CHAR));
		mTable.put("real", new Integer(sym.REAL));
		mTable.put("boolean", new Integer(sym.BOOLEAN));
		mTable.put("while", new Integer(sym.WHILE));
		mTable.put("else", new Integer(sym.ELSE));
		mTable.put("or", new Integer(sym.OR));
		mTable.put("and", new Integer(sym.AND));
		
	}
	
	/**
	 * Vraca ID kljucne reci 
	 */
	public int find(String keyword)
	{
		Object symbol = mTable.get(keyword);
		if (symbol != null)
			return ((Integer)symbol).intValue();
		
		// Ako rec nije pronadjena u tabeli kljucnih reci radi se o identifikatoru
		return sym.ID;
	}
	

}
