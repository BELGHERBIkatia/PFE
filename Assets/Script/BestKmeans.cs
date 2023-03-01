using System;
using System.Collections.Generic;
using Accord.MachineLearning;
using Accord.Math;
using Accord.Math.Distances;
using Accord.MachineLearning.Clustering;
using UnityEngine;

public class BestKmeans : MonoBehaviour
{
	public int MinK = 2; // Minimum number of clusters to consider
	public int MaxK = 10; // Maximum number of clusters to consider
	public int MaxIterations = 100; // Maximum number of iterations for the KMeans algorithm

	private List< UnityEngine.Vector3> points = new List< UnityEngine.Vector3>(); // List of data points


	void Start()
	{
		
		// Poids égaux pour chaque point de données

		// Generate some random data points
		for (int i = 0; i < 100; i++)
		{
			points.Add(new UnityEngine.Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
		}
		double[][] data = new double[ points.Count][];
		double[] weights = new double[data.Length];

		for (int i = 0; i <  data.Length; i++)
		{
			data[i] = new double[] {  points[i].x,  points[i].y,  points[i].z };
		}
		for (int i = 0; i < weights.Length; i++)
		{
			weights[i] = 1.0;
		}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		// Find the optimal number of clusters using the Silhouette algorithm
		int bestK = 0;
		double bestScore = double.NegativeInfinity;

		for (int k = MinK; k <= MaxK; k++)
		{
			var kMeans = new KMeans(k)
			{
				Distance = new Euclidean(),
				Tolerance = 0.0001,
				MaxIterations = MaxIterations
			};

			// Application de l'algorithme K-Means
			var clusters = kMeans.Learn(data,weights );
			var labels = kMeans.Clusters.Decide(data, weights);
			// Calcul du score de silhouette

			//var silhouette = new Silhouette(points.ToArray(), labels, new Euclidean()).Score;
			var silhouette = ComputeSilhouette ( labels, points);
			Console.WriteLine("K = " + k + ", Silhouette score = " + silhouette);

			if (silhouette > bestScore)
			{
				bestScore = silhouette;
				bestK = k;
			}
		}
		Console.WriteLine("bestK = " + bestK+ ", bestScore = " + bestScore);
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Create the KMeans clustering model with the optimal number of clusters
		var bestKMeans = new KMeans(bestK)
		{
			Distance = new Euclidean(),
			Tolerance = 0.0001,
			MaxIterations = MaxIterations
		};

		// Fit the model to the data points
		var bestClusters = bestKMeans.Learn(data,weights);

		// Get the cluster labels for each data point
		var bestLabels = bestKMeans.Clusters.Decide(data,weights);


	}


	public double ComputeSilhouette (double[] labels, List< UnityEngine.Vector3>  data )

	{
		int n = labels.Length; 
		double SilhouetteScore = 0 ;
		double cptlabel = 0.0;
		int c = 0;

		while( labels[c] !=labels[n] ){
			cptlabel = cptlabel + 1.0;
			c = c + 1;
		}


		for (int i = 0; i <= data.Count; i++)
		{
			double ai = 0;
			double bi = double.MaxValue;
			int cpta = 0;
		


			for (int j = 0; j < data.Count; j++)
			{
				if (labels [i] == labels [j] && i != j)
				{
					ai +=  UnityEngine.Vector3.Distance (data[i], data[j]);
					cpta = cpta + 1;
				}

			}
			ai = ai / Math.Max (1, cpta);

			for (double k = 0.0; k < cptlabel; k++)
			{
				
				if (labels [i] != k)
				{
					double dist = 0;
					int cptb = 0;
					for (int l = 0; l < data.Count; l++) 
					{
						
						if (k == labels [l])
						{
							dist += UnityEngine.Vector3.Distance (data[i], data[l]); 
							cptb = cptb + 1;
						}

						
					}

					dist = dist / cptb;
					if (dist < bi)
					{
						bi = dist;
					}
				
				}
			}


			double si = (bi - ai) / Math.Max (ai, bi);

			SilhouetteScore += si;

	}

		SilhouetteScore = SilhouetteScore / data.Count;
		return SilhouetteScore;
}
}

