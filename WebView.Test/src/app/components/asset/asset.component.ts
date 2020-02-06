import { Component, OnInit } from '@angular/core';
import { AssetsService } from 'src/app/services/assets.service';
import { filter } from 'rxjs/operators';
import { Asset } from 'src/app/models/asset';

@Component({
  selector: 'app-asset',
  templateUrl: './asset.component.html',
  styleUrls: ['./asset.component.scss']
})
export class AssetComponent implements OnInit {
  public assetsData: Asset[];
  public totalPages = 0;
  public currentPage = 1;
  constructor(private assetsService: AssetsService) { }

  ngOnInit() {
    this.assetsService.assets$.pipe(
      filter((result) => result && !!result.Assets.length)).subscribe((data) => {
        console.log(data);
        this.currentPage = data.currentPage;
        this.totalPages = data.totalPages;
        this.assetsData = data.Assets;
      });
    this.assetsService.getAssets(1);
  }

  public pageChanged(pageNumber: number) {
    this.assetsData = [];
    this.assetsService.getAssets(pageNumber);
  }

  public getPageNumber(input: number) {
    if (this.currentPage === 1) {
      return input + 1;
    } else {
      return ((this.currentPage - 1) * 20) + input + 1;
    }
  }

  public handleDeleteAsset(assetId: string) {
    if (confirm('Are sure want to delete the record?')) {
      this.assetsService.DeleteAsset(assetId).subscribe((data) => {
        alert('Deleted Successfully! Reloading Data.');
        this.pageChanged(this.currentPage);
      });
    }
  }
}
