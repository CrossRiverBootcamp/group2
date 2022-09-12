import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Operation } from 'src/app/models/operation';
import { OperationService } from 'src/app/services/operation.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { Account } from 'src/app/models/Account';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class OperationsHistoryComponent implements AfterViewInit {
  operationsData: Operation[] = [];
  visitedPages: number[] = [0];
  pageSize = 5;
  currentPage = 0;
  loading: boolean = false;
  displayedColumns: string[] = ['amount', 'balance', 'date'];
  columnsToDisplayWithExpand = [...this.displayedColumns, 'expand'];
  expandedElement: Account | undefined | null;

  dataSource!: MatTableDataSource<Operation>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private operationService: OperationService,
    private userService: UserService
  ) {}

  ngAfterViewInit(): void {
    this.loadData();
  }

  loadData() {
    this.loading = true;
    let accountId = this.userService.getAccountID();
    if (accountId)
      this.operationService
        .getOperations(
          accountId,
          this.currentPage * this.pageSize,
          this.pageSize + 1
        )
        .subscribe(
          (res) => {
            this.operationsData = [...this.operationsData, ...res];
            this.dataSource = new MatTableDataSource(this.operationsData);
            this.dataSource.paginator = this.paginator;
            this.paginator.pageIndex = this.currentPage;
            this.paginator.length = res.length;
            this.dataSource.sort = this.sort;
            this.visitedPages.push(this.currentPage);
            this.loading = false;
          },
          (err) => {
            console.log(err);
            this.loading = false;
          }
        );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;

    if (
      (!event.previousPageIndex || event.previousPageIndex < event.pageIndex) &&
      this.visitedPages.indexOf(event.pageIndex) == -1
    ) {
      console.log(`loading Data for page ${event.pageIndex}...`);
      this.loadData();
    }
  }
}
