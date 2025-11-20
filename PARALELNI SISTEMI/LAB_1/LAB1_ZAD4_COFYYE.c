#include <stdio.h>
#include <mpi.h>

constexpr auto N = 6;

int main(int argc, char** argv) {
	int rank = -1, P = -1, root = 0;
	float niz[N] = { 3, 5, 8, 2, 1, 9 }, local_niz[N]{}, minimalni_niz[N]{}, min = 0;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &P);
	MPI_Status status{};

	if (N % P != 0) {
		return -1;
	}

	if (rank == root) {
		min = niz[0];
		for (int i = 1; i < N / P; i++) {
			if (min > niz[i]) {
				min = niz[i];
			}
		}
		
		minimalni_niz[root] = min;

		for (int i = 1; i < P; i++) {
			MPI_Send(niz + (N / P) * i, N / P, MPI_FLOAT, i, 0, MPI_COMM_WORLD);
			MPI_Recv(minimalni_niz + i, 1, MPI_FLOAT, i, 1, MPI_COMM_WORLD, &status);
		}

		min = minimalni_niz[0];
		for (int i = 1; i < P; i++) {
			if (min > minimalni_niz[i]) {
				min = minimalni_niz[i];
			}
		}

		printf("Minimalni element je %f", min);
	}
	else {
		MPI_Recv(local_niz, N / P, MPI_FLOAT, root, 0, MPI_COMM_WORLD, &status);
		
		min = local_niz[0];
		for (int i = 1; i < N / P; i++) {
			if (min > local_niz[i]) {
				min = local_niz[i];
			}
		}

		MPI_Send(&min, 1, MPI_FLOAT, root, 1, MPI_COMM_WORLD);
	}

	MPI_Finalize();
	
	return 0;
}