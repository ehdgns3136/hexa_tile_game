using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class HexMetrics {

	public const float outerRadius = 10f;

	public const float innerRadius = outerRadius * 0.866025404f;
	
	public static Vector3[] corners = {
		new Vector3(0f, outerRadius),
		new Vector3(innerRadius, 0.5f * outerRadius),
		new Vector3(innerRadius, -0.5f * outerRadius),
		new Vector3(0f, -outerRadius),
		new Vector3(-innerRadius, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0.5f * outerRadius),
		new Vector3(0f, outerRadius)
	};
}

public class HexGrid : MonoBehaviour
{
	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;
	
	HexCell[] cells;

	public Text cellLabelPrefab;

	Canvas gridCanvas;
	HexMesh hexMesh;
	
	public Color defaultColor = Color.white;
	public Color touchedColor = Color.magenta;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int y = 0, i = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, y, i++);
			}
		}
	}
	
	void Start () {
		hexMesh.Triangulate(cells);
	}
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			HandleInput();
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
			TouchCell(hit.point);
		}
	}
	
	void TouchCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = touchedColor;
		hexMesh.Triangulate(cells);	
		
		Debug.Log("touched at " + coordinates.ToString());
	}
	
	void CreateCell (int x, int y, int i) {
		Vector3 position;
		position.x = (x + (y & 1) / 2f) * (HexMetrics.innerRadius * 2f);
		position.y = y * HexMetrics.outerRadius * 1.5f;
		position.z = 0f;

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
		cell.color = defaultColor;
		
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
		label.text = cell.coordinates.ToStringOnSeparateLines();
	}
}
