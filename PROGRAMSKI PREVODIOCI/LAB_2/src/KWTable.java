
import java.util.Hashtable;
public class KWTable {

	private Hashtable<String, Integer> mTable;
	public KWTable()
	{
		// Inicijalizcaija hash tabele koja pamti kljucne reci
		mTable = new Hashtable<String, Integer>();
		mTable.put("program", Integer.valueOf(sym.PROGRAM));
		mTable.put("float", Integer.valueOf(sym.FLOAT));
		mTable.put("int", Integer.valueOf(sym.INT));
		mTable.put("char", Integer.valueOf(sym.CHAR));
		mTable.put("end", Integer.valueOf(sym.END));
	}
	
	/**
	 * Vraca ID kljucne reci 
	 */
	public int find(String keyword)
	{
		Integer symbol = mTable.get(keyword);
		if (symbol != null)
			return symbol.intValue();
		
		// Ako rec nije pronadjena u tabeli kljucnih reci radi se o identifikatoru
		return sym.ID;
	}
	

}
